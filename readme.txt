potrzebne sdk zainstalowane: .net core 3.1 
użyta baza danych: mssql

ustawianie komunikacji z bazą danych: należy zmodyfikować plik "appsetings.json", i zmodyfikować "DefaultConnection" na connection stringa któy pozwoli się połączyć z bazą danych na komupterze

komendy w powershellu:
add-migration <nazwa migracji>
update-database
