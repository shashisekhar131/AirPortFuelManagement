namespace AirportFuelManagementWebAPI.Utils
{
    public class Logger : ILogger
    {
        private readonly string _folderpath;

        public Logger(string folderpath)
        {
            _folderpath = folderpath;
        }

        public void LogException(Exception ex)
        {
            string fileName = DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            try
            {

                if (!Directory.Exists(_folderpath))
                {
                    Directory.CreateDirectory(_folderpath);
                }

                string path = Path.Combine(_folderpath, fileName);
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(ex.ToString());
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}
