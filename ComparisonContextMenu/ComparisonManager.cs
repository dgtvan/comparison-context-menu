using System.IO;
using System.Linq;

namespace ComparisonContextMenu
{
    public static class ComparisonManager
    {
        public static void SetFirstPath(string path)
        {
            ComparisonItem item = new ComparisonItem()
            {
                Path = path,
                Type = IsFolder(path) ? ComparisonItemType.Folder : ComparisonItemType.File
            };

            ComparisonDataManager.Set(item);
        }

        public static string GetFirstPath()
        {
            ComparisonItem item = ComparisonDataManager.Get();

            return item?.Path;
        }

        public static bool CanCompare(string pathOne, string pathTwo)
        {
            if (string.IsNullOrEmpty(pathOne) || string.IsNullOrEmpty(pathTwo))
                return false;

            int count =
                new string[]
                {
                    pathOne,
                    pathTwo
                }
                .Select(path => IsFolder(path))
                .Where(isFolder => isFolder)
                .Count();

            return count == 0 || count == 2;
        }

        public static void Compare(string pathOne, string pathTwo)
        {
            RiderExecutor.Execute(pathOne, pathTwo);
        }

        public static void ResetState()
        {
            ComparisonDataManager.Set(new ComparisonItem());
        }

        private static bool IsFolder(string path)
        {
            return Utility.IsFolder(path);
        }
    }
}