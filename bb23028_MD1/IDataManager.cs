namespace bb23028_MD1;

public interface IDataManager
{

    string Print(); //tika nomainīts no tipa void uz string, lai to varētu izmantot kā label text MAUI programmā
    void Save(string path);
    void Load(string path);
    void CreateTestData();
    void Reset();


}
