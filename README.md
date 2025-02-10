Visas MD versijas atrodas branches izņemot sākotnējā (MD1).
Tutorial priekš md3:
1) appsettings ielikt savu connection string db ar savu db nosaukumu
2) uzstadi md1 par startup projektu for now (jo tur ir db)
3) build md1
4) aizej uz package manager console
5) uzraksti komandu Add-Migration [MigrationName]
-- sis iveidos migration --
6) Pēc tam kad izveido migrāciju tajā pašā vietā kur ievadīji migration, ievadi:
   Update-Database -Connection "[šeit jāiekopē tavs connection string]"

Pēc tam var paskatīties vai db parādijās visas tabulas kas ir klasē
