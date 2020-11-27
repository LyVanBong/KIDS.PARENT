﻿using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Models.Login;
using Realms;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KIDS.MOBILE.APP.PARENTS.Services.Database
{
    public class DatabaseService :IDatabaseService
    {
        private RealmConfiguration _realmConfiguration;
        public DatabaseService()
        {
            _realmConfiguration = new RealmConfiguration(AppConstants.RealmConfiguration);
            _realmConfiguration.EncryptionKey = new byte[64]
            {
                0x02, 0x03, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                0x12, 0x13, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18,
                0x22, 0x23, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28,
                0x32, 0x33, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38,
                0x42, 0x43, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48,
                0x52, 0x53, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58,
                0x62, 0x63, 0x63, 0x64, 0x65, 0x66, 0x67, 0x68,
                0x72, 0x73, 0x73, 0x74, 0x75, 0x76, 0x77, 0x78
            };
        }
        private Realm GetInstance()
        {
            return Realm.GetInstance(_realmConfiguration);
        }
        public Task<bool> SetAccount(UserModel user)
        {
            try
            {
                if (user != null)
                {
                    var realm = GetInstance();
                    realm.Write(() =>
                    {
                        realm.Add(user);
                    });
                    return Task.FromResult(true);
                }
                else
                    return Task.FromResult(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Task<UserModel> GetAccount()
        {
            try
            {
                var realm = GetInstance();
                var data = realm.All<UserModel>();
                if (data != null && data.Any())
                {
                    return Task.FromResult(data.FirstOrDefault());
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Task<bool> DeleteAccount()
        {
            try
            {
                var realm = GetInstance();
                realm.Write(() =>
                {
                    realm.RemoveAll<UserModel>();
                });
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Task<bool> UpdateAccount(UserModel user)
        {
            try
            {
                if (user != null)
                {
                    var realm = GetInstance();
                    realm.Write(() =>
                    {
                        realm.RemoveAll<UserModel>();
                        realm.Add(user);
                    });
                    return Task.FromResult(true);
                }
                else
                    return Task.FromResult(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
