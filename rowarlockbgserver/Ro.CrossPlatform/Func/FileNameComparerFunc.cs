namespace Ro.CrossPlatform.Func;

public class FileNameComparerFunc : IComparer<FileInfo>
{
    /// <summary>
    /// 比较文件名
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public int Compare(FileInfo? x, FileInfo? y)
    {
        // 将文件名称按照 "." 进行分割
        if (x != null && y != null)
        {
            // 获取文件名
            string xName = Path.GetFileNameWithoutExtension(x.Name);
            string yName = Path.GetFileNameWithoutExtension(y.Name);
            string[] xParts = xName.Split('.');
            string[] yParts = yName.Split('.');

            // 依次比较每个部分的数值
            for (int i = 0; i < Math.Min(xParts.Length, yParts.Length); i++)
            {
                int xValue = int.Parse(xParts[i]);
                int yValue = int.Parse(yParts[i]);
                if (xValue != yValue) return xValue.CompareTo(yValue);
            }

            // 如果所有部分都相等，则长度更长的文件名称排在后面
            return xParts.Length.CompareTo(yParts.Length);
        }
        else
        {
            return 0;
        }
    }
}