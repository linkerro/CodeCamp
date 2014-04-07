using System.IO;
using System.IO.IsolatedStorage;

namespace CodeCampWP7
{
    public class Storage
    {
        /// <summary>
        /// Save content to the given file name. If the file exists it is replaced by the new one, if it does not exist it is created.
        /// </summary>
        /// <param name="fileName">The name of the file to be created.</param>
        /// <param name="content">The string content to be saved to disk.</param>
        public void SaveToStorage(string fileName, string content)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                //if the file exists delete it, as we don't need the old version
                if (store.FileExists(fileName))
                    store.DeleteFile(fileName);

                using (var file = store.OpenFile(fileName, FileMode.Create, FileAccess.Write))
                {
                    using (var streamWriter = new StreamWriter(file))
                    {
                        streamWriter.Write(content);
                        streamWriter.Flush();
                    }
                }

            }

        }

        /// <summary>
        /// Load content form the given file.
        /// </summary>
        /// <param name="fileName">The name of the file that needs to be read.</param>
        /// <returns>The content of the file as a string.</returns>
        public string LoadFromStorage(string fileName)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!store.FileExists(fileName))
                    return null;

                using (var file = store.OpenFile(fileName, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new StreamReader(file))
                    {
                        return reader.ReadToEnd();
                    }
                }

            }
        }
    }
}