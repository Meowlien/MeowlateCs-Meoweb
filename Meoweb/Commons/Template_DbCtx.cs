﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Meoweb.Commons {

    /// <summary>
    /// 資料庫上下文-模板
    /// </summary>
    public abstract class DbCtxTemplate : DbContext {

        protected ILogger Logger { get; set; }


        public DbCtxTemplate(ILogger logger) {
            Logger = logger;
        }
        public DbCtxTemplate(DbContextOptions options, ILogger logger)
            : base(options) {
            Logger = logger;
        }


        // 連綫
        protected abstract void Connect(DbContextOptionsBuilder optionsBuilder);

        // 設定
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // 僅配置一次
            if (optionsBuilder.IsConfigured == false) {
                /*  設定連綫指令(需要在子類中提供連綫方式，即：選擇資料庫類型)
                * -------------------------------------------------------
                *   UseSqlServer            用於連接到 Microsoft SQL Server 資料庫。
                *   UseNpgsql               用於連接到 PostgreSQL 資料庫。
                *   UseMySQL                用於連接到 MySQL 資料庫。
                *   UseSqlite               用於連接到 SQLite 資料庫。
                *   UseInMemoryDatabase     用於使用內存資料庫進行測試或臨時存儲。
                */
                Connect(optionsBuilder);
            }
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess) {
            Logger.LogInformation("Saving changes to the database");
            int result = base.SaveChanges(acceptAllChangesOnSuccess);
            Logger.LogInformation("Changes saved to the database");
            return result;
        }

    }
}
