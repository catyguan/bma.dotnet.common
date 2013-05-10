using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bmalangutil.core
{
    public class VPath
    {
        private String[] path;
	    private static String[] EMPTY_PATH = new String[0];
	    private static VPath ROOT = new VPath();
	    private static VPath EMPTY = new VPath();

	    /**
	     * 路径分割符号
	     */
	    public const char separatorChar = '/';        
        public const String separator = "/";
        public static char[] separatorChars = new Char[] { separatorChar };

	    /**
	     * 表示当前路径
	     */
	    public const String CURRENT_PATH = ".";

	    /**
	     * 表示上一层路径
	     */
	    public const String PARENT_PATH = "..";

        public VPath() {
		    this.path = EMPTY_PATH;
	    }

	    public static String[] split(String path) {
		    return split(path, separatorChars);
	    }

	    public static String[] split(String path, char[] sp) {
            if(path==null || path=="")return EMPTY_PATH;
            String[] slist = path.Split(sp);
            return slist;
	    }

	    public static VPath create(String fullPath, char[] separator) {
		    if (fullPath == null || fullPath=="") {
			    return root();
		    }
		    VPath reVPath = new VPath();
		    reVPath.path = split(fullPath, separator);
		    return reVPath;
	    }

	    /**
	     * 按照指定的路径全称创建VPath
	     * 
	     */
	    public static VPath create(String fullPath) {
		    return create(fullPath, separatorChars);
	    }

	    /**
	     * 使用字符串（路径名成）数组创建路径
	     * 
	     */
	    public static VPath create(String[] paths) {
		    VPath returnPath = new VPath();
		    if (paths != null) {
			    returnPath.path = paths;
		    }
		    return returnPath;
	    }

	    public static VPath create(String[] paths, int start, int len) {
		    VPath returnPath = new VPath();
		    if (paths != null) {
			    returnPath.path = new String[len];
                Array.Copy(paths, start, returnPath.path, 0, len);
		    }
		    return returnPath;
	    }

	    /**
	     * 获取根路径
	     * 
	     */
	    public static VPath root() {
		    return ROOT;
	    }

	    /**
	     * 在当前的路径后添加子路径
	     * 
	     */
	    public VPath add(String[] subPath) {
		    if (subPath == null) {
			    return this;
		    }

		    String[] returnPath = new String[this.path.Count() + subPath.Count()];

		    Array.Copy(this.path, returnPath, this.path.Count());
		    Array.Copy(this.path,this.path.Count(),subPath,0,subPath.Count());
		    VPath reVPath = new VPath();
		    reVPath.path = returnPath;
		    return reVPath;
	    }

	    /**
	     * 在当前的路径后添加子路径
	     * 
	     */
	    public VPath add(String subPath) {
		    return add(split(subPath));
	    }

	    /**
	     * 在当前的路径后添加子路径
	     * 
	     */
	    public VPath add(VPath subpath) {
		    return add(subpath.path);
	    }

	    /**
	     * 获得子路径
	     * 
	     */
	    public VPath subpath(int idx) {
		    if (idx >= this.path.Count())
			    return EMPTY;
		    if (idx >= this.path.Count())
			    return EMPTY;
		    return create(path, idx, this.path.Count()- idx);
	    }

	    public VPath subpath(int start, int end) {
		    if (start >= this.path.Count())
			    return EMPTY;
		    end = end < this.path.Count() - 1 ? end : this.path.Count() - 1;
		    if (start >= end)
			    return EMPTY;
		    int len = end - start;
		    if (len == 0)
			    return EMPTY;
		    return create(path, start, len);
	    }

	    /**
	     * 获取当前路径相对于rootPath的子路径资料（注：不包含rootPath）
	     * 
	     */
	    public VPath subpath(String rootPath) {
		    return subpath(rootPath, true);
	    }

	    /**
	     * 获取当前路径相对于rootPath的子路径资料（注：不包含rootPath）
	     * 
	     */
	    public VPath subpath(String rootPath, bool allMatch) {
		    return subpath(VPath.create(rootPath), allMatch);
	    }

	    public VPath subpath(VPath rootPath, bool allMatch) {
		    int i;
		    for (i = 0; i < rootPath.path.Count(); i++) {
			    bool m = false;
			    if (i < this.path.Count()
					    && rootPath.path[i]==this.path[i]) {
				    m = true;
			    }
			    if (!m) {
				    if (allMatch) {
					    return null;
				    }
				    break;
			    }
		    }
		    VPath reVPath = new VPath();
		    if (i < this.path.Count()) {
			    reVPath.path = new String[this.path.Count() - i];
			    Array.Copy(this.path, i, reVPath.path, 0, this.path.Count() - i);
		    }
		    return reVPath;
	    }

	    /**
	     * 把当前路径按照分割符切割为字符串数组
	     * 
	     */
	    public String[] split(int limit) {
		    String fullPath = this.ToString();
		    return fullPath.Split(separatorChars, limit);
	    }

	    public String[] split() {
		    return this.path;
	    }

        public override string ToString()
        {
 	        return toString(true);
        }

	    public String toString(bool root) {
		    return toString(separator, root);
	    }

	    /**
	     * 获取该路径的字符串形式
	     */
	    public String toString(String sp, bool root) {
		    StringBuilder fullPath = new StringBuilder(80);
		    if (root) {
			    fullPath.Append(sp);
		    }
		    for (int i = 0; i < this.path.Count() ; i++) {
			    if (i != 0) {
				    fullPath.Append(sp);
			    }
			    fullPath.Append(this.path[i]);
		    }
		    return fullPath.ToString();
	    }

	    /**
	     * 获取路径名称
	     * 
	     */
	    public String Name
        {
            get {
		        int num = this.path.Count();
		        if (num > 0) {
			        return this.path[num - 1];
		        } else {
			        return "";
		        }
            }
            set
            {
                if (this.path.Count()> 0) {
			        VPath np = copy();
			        np.path[np.path.Count() - 1] = value;			        
		        } else {
			        create(value);
		        }    
            }
	    }

	    public String getPathName(int idx) {
		    if (idx < this.path.Count()) {
			    return this.path[idx];
		    }
		    return null;
	    }

	    /**
	     * 获取父路径
	     * 
	     */
	    public VPath getParent() {
		    VPath repath = new VPath();
		    int num = this.path.Count() - 1;
		    if (num > 0) {
			    String[] strPath = new String[num];
			    for (int i = 0; i < num; i++) {
				    strPath[i] = this.path[i];
			    }
			    repath.path = strPath;
			    return repath;
		    } else {
			    return root();
		    }
	    }

	    /**
	     * 	
	     */
	    public bool isParantOf(VPath child) {
		    int level = 0;
		    for (; level < this.path.Count(); level++) {
			    if (level >= child.path.Count()) {
				    return false;
			    }
			    if (child.path[level]!=this.path[level]) {
				    return false;
			    }
		    }
		    return true;
	    }

        public override bool Equals(object obj)
        {
 	        if (base.Equals(obj)) {
			    return true;
		    }
		    if (obj is VPath) {
			    VPath objPath = obj as VPath;
			    if (objPath.path.Count() != this.path.Count()) {
				    return false;
			    }
			    for (int i = 0; i < this.path.Count() ; i++) {
				    if (!objPath.path[i].Equals(this.path[i])) {
					    return false;
				    }
			    }
			    return true;
		    }
		    return false;
        }

        public override int GetHashCode()
        {
            int code = 0;
            foreach (String s in this.path)
            {
                code += s.GetHashCode();
            }
            return code;
        }

	    /**
	     * 测试是否是绝对路径，即从'/'开始
	     * 
	     */
	    public static bool isRooted(String path) {
		    if (path.Count() > 0 && path[0] == separatorChar) {
			    return true;
		    }
		    return false;
	    }

	    public bool isRoot() {
		    return this.pathLevel() == 0;
	    }

	    /**
	     * 根据当前路径,分析指定的路径<br>
	     * 若paths是从根开始，则返回paths的VPath形式；否则，把paths增加到this的路径下<br>
	     * 
	     */
	    public VPath resolve(String paths) {
		    if (VPath.isRooted(paths)) {
			    return create(paths);
		    } else {
			    return add(paths);
		    }
	    }

	    /**
	     * 如果路径层次中有[.]则跳过,[..]则返回上一层
	     * 
	     * @return
	     */
	    public VPath parse() {

		    bool parse = false;
		    foreach (String p in this.path) {
			    if (p.Equals(CURRENT_PATH) || p.Equals(PARENT_PATH)) {
				    parse = true;
				    break;
			    }
		    }

		    if (!parse) {
			    return this;
		    }

		    LinkedList<String> returnPath = new LinkedList<String>();
		    foreach (String p in this.path) {
			    if (p.Equals(CURRENT_PATH)) {
				    continue;
			    }
			    if (p.Equals(PARENT_PATH)) {
				    if (returnPath.Count()>0) {
					    returnPath.RemoveLast();
				    }
				    continue;
			    }
                returnPath.AddLast(p);
		    }
		    return create(returnPath.ToArray());
	    }

	    /**
	     * 根据当前路径,获取解析到(resolve)指定的路径所需的路径字符串<br>
	     * 
	     * VPath("/t1/t2/t3").resolve("/t1/t2/t4") = "../t4"
	     * VPath("/t1/t2/t3").resolve("/t1/t2/t3/t4") = "t4"
	     * 
	     * basePath.resolve(basePath.getRelative(path1)).parse() == path1
	     * 
	     */
	    public String getRelative(VPath relPath) {
		    return getRelative(relPath, true);
	    }

	    public String getRelative(String relPath, bool meetAtRoot) {
		    return getRelative(VPath.create(relPath), meetAtRoot);
	    }

	    public String getRelative(VPath relPath, bool meetAtRoot) {
		    int level = 0; // 相同的层次
		    for (; level < relPath.path.Count() && level < this.path.Count(); level++) {
			    if (!relPath.path[level].Equals(this.path[level])) {
				    break;
			    }
		    }

		    if (level == 0 && meetAtRoot) {
			    // meet at ROOT!!
			    return relPath.ToString();
		    }

            StringBuilder buf = new StringBuilder(80);
		    // build PARENT_PATHs
		    for (int i = level; i < this.path.Count(); i++) {
			    if (buf.Length > 0) {
				    buf.Append(separatorChar);
			    }
			    buf.Append(PARENT_PATH);
		    }
		    // build sub path
		    for (int i = level; i < relPath.path.Count(); i++) {
			    if (buf.Length > 0) {
				    buf.Append(separatorChar);
			    }
			    buf.Append(relPath.path[i]);
		    }

		    return buf.ToString();
	    }

	    /**
	     * 获取路径的长度
	     * 
	     */
	    public int pathLevel() {
		    return this.path.Count();
	    }

	    public VPath copy() {
		    String[] newPath = new String[this.path.Count()];
		    for (int i = 0; i < this.path.Count(); i++) {
			    newPath[i] = this.path[i];
		    }
		    return VPath.create(newPath);
	    }

	    public VPath appendName(String name) {
		    VPath np = copy();
		    if (np.path.Count() != 0) {
			    np.path[np.path.Count() - 1] = np.path[np.path.Count() - 1] + name;
		    }
		    return np;
	    }

	    public VPath addExt(String extName) {
		    return appendName(extName);
	    }

	    public VPath setExt(String extName) {
		    VPath np = copy();
		    if (np.path.Count() != 0) {
			    String name = np.path[np.path.Count() - 1];
			    if (name != null) {
				    int idx = name.LastIndexOf('.');
				    if (idx != -1) {
					    name = name.Substring(0, idx) + extName;
				    } else {
					    name = name + extName;
				    }
				    np.path[np.path.Count() - 1] = name;
			    }
		    }
		    return np;
	    }

	    public String getExt() {
		    String name = Name;
		    if (name!=null && name!="") {
			    int idx = name.LastIndexOf('.');
			    if (idx != -1) {
				    return name.Substring(idx);
			    }
		    }
		    return "";
	    }

	    public String bindRootFile(String root) {
            String p = toString(false);
            if (root == null) return toString(false);
            if (!root.EndsWith("\\"))
            {
                return root + "\\" + p;
            }
            return root + p;
	    }
	    
    }
}
