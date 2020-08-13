using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CarsCatalog.Db
{
    public class FileEntityStorage : InMemoryEntityStorage
    {
        private readonly FileInfo dbFile;
        private readonly JsonSerializer jsonSerializer;

        public FileEntityStorage(string fileAbsolutePath)
        {
            dbFile = new FileInfo(fileAbsolutePath);
            jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Formatting.None,
                Converters = new List<JsonConverter> {new StringEnumConverter()}
            });

            StorageImpl = LoadFromFile() ?? new MemoryStorageImpl();
            StorageImpl.OnUpdated += SaveToFile;
        }

        private void SaveToFile()
        {
            dbFile.Delete();
            using var fs = dbFile.OpenWrite();
            using var sw = new StreamWriter(fs);
            using var jsonWriter = new JsonTextWriter(sw);
            jsonSerializer.Serialize(jsonWriter, StorageImpl);
        }

        private MemoryStorageImpl LoadFromFile()
        {
            if (!dbFile.Exists)
                return null;

            using var fs = dbFile.OpenRead();
            using var sr = new StreamReader(fs);
            using var jsonReader = new JsonTextReader(sr);
            return jsonSerializer.Deserialize<MemoryStorageImpl>(jsonReader);
        }
    }
}