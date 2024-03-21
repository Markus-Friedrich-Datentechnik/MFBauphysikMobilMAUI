using MFBauphysikMobilMAUI.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace MFBauphysikMobilMAUI.Data
{
    public class Database
    {
        SQLiteAsyncConnection _database;
        //public Database(string dbPath)
        public Database(string dbPath)
        {
           /*_database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<MainModel>();
            _database.CreateTableAsync<BefestigerBasis>();
            _database.CreateTableAsync<Basis>();
            _database.CreateTableAsync<BefestigerSparren>();
            _database.CreateTableAsync<Sparren>();
            _database.CreateTableAsync<BefestigerStänder>();
            _database.CreateTableAsync<Ständer>();
            _database.CreateTableAsync<BefestigerGefach>();
            _database.CreateTableAsync<Gefach>();
            _database.CreateTableAsync<EinstellungModel>();*/
         
        }
        async Task Init()
        {
            if (_database is not null)
                return;
            _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await _database.CreateTableAsync<MainModel>();
            await _database.CreateTableAsync<BefestigerBasis>();
            await _database.CreateTableAsync<Basis>();
            await _database.CreateTableAsync<BefestigerSparren>();
            await _database.CreateTableAsync<Sparren>();
            await _database.CreateTableAsync<BefestigerStänder>();
            await _database.CreateTableAsync<Ständer>();
            await _database.CreateTableAsync<BefestigerGefach>();
            await _database.CreateTableAsync<Gefach>();
            await _database.CreateTableAsync<EinstellungModel>();
        }
        //Create project
        public async Task<List<MainModel>> GetItemAsync()
        {
            await Init();
            return await _database.Table<MainModel>().ToListAsync();

        }
        public async Task<MainModel> GetItemAsync(int id)
        {
            await Init();
            return await _database.Table<MainModel>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        public async Task<int> SaveItemAsync(MainModel item)
        {
            await Init();
            return await _database.InsertAsync(item);
        }
        public async Task<int> UpdateItemAsync(MainModel item)
        {
            await Init();
            return await _database.UpdateAsync(item);
        }
       
        public async Task<int> DeleteItems(MainModel item)
        {
            await Init();
            return await _database.DeleteAsync(item);
        }

        //Befestiger Basis
        public async Task<List<BefestigerBasis>> GetFixAsync()
        {
            await Init();
            return await _database.Table<BefestigerBasis>().ToListAsync();
        }
        public async Task<BefestigerBasis> GetFixAsync(int id)
        {
            await Init();
            return await _database.Table<BefestigerBasis>().Where(i => i.ID_Befestiger == id).FirstOrDefaultAsync();
        }
        public async Task<int> SaveFixAsync(BefestigerBasis item)
        {
            await Init();
            return await _database.InsertAsync(item);
        }
        public async Task<int> UpdateFixAsync(BefestigerBasis item)
        {
            await Init();
            return await _database.UpdateAsync(item);
        }
        public async Task<int> DeleteFixItems(BefestigerBasis item)
        {
            await Init();
            return await _database.DeleteAsync(item);
        }
        public async Task<int> DeleteAllFix<T>()
        {
            await Init();
            return await _database.DeleteAllAsync<BefestigerBasis>();
        }


        //Bauteile Basis
        public async Task<List<Basis>> GetBauteilAsync()
        {
            await Init();
            return await _database.Table<Basis>().ToListAsync();
        }
        public async Task<Basis> GetBauteilAsync(int id)
        {
            await Init();
            return await _database.Table<Basis>().Where(i => i.ID_Bauteil == id).FirstOrDefaultAsync();
        }
        public async Task<int> SaveBauteilAsync(Basis item)
        {
            await Init();
            return await _database.InsertAsync(item);
        }
        public async Task<int> UpdateBauteilAsync(Basis item)
        {
            await Init();
            return await _database.UpdateAsync(item);
        }
        public async Task<int> DeleteBauteilItems(Basis item)
        {
            await Init();
            return await _database.DeleteAsync(item);
        }
        public async Task<int> DeleteAllBauteil<T>()
        {
            await Init();
            return await _database.DeleteAllAsync<Basis>();
        }


        //Befestiger Sparren
        public async Task<List<BefestigerSparren>> GetFixSparrenAsync()
        {
            await Init();
            return await _database.Table<BefestigerSparren>().ToListAsync();
        }
        public async Task<BefestigerSparren> GetFixSparrenAsync(int id)
        {
            await Init();
            return await _database.Table<BefestigerSparren>().Where(i => i.ID_Befestiger == id).FirstOrDefaultAsync();
        }
        public async Task<int> SaveFixSparrenAsync(BefestigerSparren item)
        {
            await Init();
            return await _database.InsertAsync(item);
        }
        public async Task<int> UpdateFixSparrenAsync(BefestigerSparren item)
        {
            await Init();
            return await _database.UpdateAsync(item);
        }
        public async Task<int> DeleteFixSparrenItems(BefestigerSparren item)
        {
            await Init();
            return await _database.DeleteAsync(item);
        }
        public async Task<int> DeleteAllFixSparren<T>()
        {
            await Init();
            return await _database.DeleteAllAsync<BefestigerSparren>();
        }


        //Bauteile Sparren
        public async Task<List<Sparren>> GetBauteilSparrenAsync()
        {
            await Init();
            return await _database.Table<Sparren>().ToListAsync();
        }
        public async Task<Sparren> GetBauteilSparrenAsync(int id)
        {
            await Init();
            return await _database.Table<Sparren>().Where(i => i.ID_Bauteil == id).FirstOrDefaultAsync();
        }
        public async Task<int> SaveBauteilSparrenAsync(Sparren item)
        {
            await Init();
            return await _database.InsertAsync(item);
        }
        public async Task<int> UpdateBauteilSparrenAsync(Sparren item)
        {
            await Init();
            return await _database.UpdateAsync(item);
        }
        public async Task<int> DeleteBauteilSparrenItems(Sparren item)
        {
            await Init();
            return await _database.DeleteAsync(item);
        }
        public async Task<int> DeleteAllSparren<T>()
        {
            await Init();
            return await _database.DeleteAllAsync<Sparren>();
        }


        //Befestiger Gefach
        public async Task<List<BefestigerGefach>> GetFixGefachAsync()
        {
            await Init();
            return await _database.Table<BefestigerGefach>().ToListAsync();
        }
        public async Task<BefestigerGefach> GetFixGefachAsync(int id)
        {
            await Init();
            return await _database.Table<BefestigerGefach>().Where(i => i.ID_Befestiger == id).FirstOrDefaultAsync();
        }
        public async Task<int> SaveFixGefachAsync(BefestigerGefach item)
        {
            await Init();
            return await _database.InsertAsync(item);
        }
        public async Task<int> UpdateFixGefachAsync(BefestigerGefach item)
        {
            await Init();
            return await _database.UpdateAsync(item);
        }
        public async Task<int> DeleteFixGefachItems(BefestigerGefach item)
        {
            await Init();
            return await _database.DeleteAsync(item);
        }
        public async Task<int> DeleteAllFixGefach<T>()
        {
            await Init();
            return await _database.DeleteAllAsync<BefestigerGefach>();
        }


        //Bauteile Gefach
        public async Task<List<Gefach>> GetBauteilGefachAsync()
        {
            await Init();
            return await _database.Table<Gefach>().ToListAsync();
        }
        public async Task<Gefach> GetBauteilGefachAsync(int id)
        {
            await Init();
            return await _database.Table<Gefach>().Where(i => i.ID_Bauteil == id).FirstOrDefaultAsync();
        }
        public async Task<int> SaveBauteilGefachAsync(Gefach item)
        {
            await Init();
            return await _database.InsertAsync(item);
        }
        public async Task<int> UpdateBauteilGefachAsync(Gefach item)
        {
            await Init();
            return await _database.UpdateAsync(item);
        }
        public async Task<int> DeleteBauteilGefachItems(Gefach item)
        {
            await Init();
            return await _database.DeleteAsync(item);
        }
        public async Task<int> DeleteAllGefach<T>()
        {
            await Init();
            return await _database.DeleteAllAsync<Gefach>();
        }


        //Befestiger Ständer
        public async Task<List<BefestigerStänder>> GetFixStänderAsync()
        {
            await Init();
            return await _database.Table<BefestigerStänder>().ToListAsync();
        }
        public async Task<BefestigerStänder> GetFixStänderAsync(int id)
        {
            await Init();
            return await _database.Table<BefestigerStänder>().Where(i => i.ID_Befestiger == id).FirstOrDefaultAsync();
        }
        public async Task<int> SaveFixStänderAsync(BefestigerStänder item)
        {
            await Init();
            return await _database.InsertAsync(item);
        }
        public async Task<int> UpdateFixStänderAsync(BefestigerStänder item)
        {
            await Init();
            return await _database.UpdateAsync(item);
        }
        public async Task<int> DeleteFixStänderItems(BefestigerStänder item)
        {
            await Init();
            return await _database.DeleteAsync(item);
        }
        public async Task<int> DeleteAllFixStänder<T>()
        {
            await Init();
            return await _database.DeleteAllAsync<BefestigerStänder>();
        }

        //Bauteile Ständer
        public async Task<List<Ständer>> GetBauteilStänderAsync()
        {
            await Init();
            return await _database.Table<Ständer>().ToListAsync();
        }
        public async Task<Ständer> GetBauteilStänderAsync(int id)
        {
            await Init();
            return await _database.Table<Ständer>().Where(i => i.ID_Bauteil == id).FirstOrDefaultAsync();
        }
        public async Task<int> SaveBauteilStänderAsync(Ständer item)
        {
            await Init();
            return await _database.InsertAsync(item);
        }
        public async Task<int> UpdateBauteilStänderAsync(Ständer item)
        {
            await Init();
            return await _database.UpdateAsync(item);
        }
        public async Task<int> DeleteBauteilStänderItems(Ständer item)
        {
            await Init();
            return await _database.DeleteAsync(item);
        }
        public async Task<int> DeleteAllStänder<T>()
        {
            await Init();
            return await _database.DeleteAllAsync<Ständer>();
        }      
    }
    
}
