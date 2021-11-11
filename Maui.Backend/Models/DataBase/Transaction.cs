﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Maui.Backend.Models.DataBase
{
    public class Transaction
    {
        /// <summary>
        /// Debe dejarse en blanco para que EF
        /// asgine su ID automáticamente.
        /// </summary>
        [Key] public uint TransactionId { get; set; }
        public uint QuantityOperated { get; set; }
        
        /// <summary>
        /// Los valores esperados son ...
        /// </summary>
        // TODO: Definir valores esperados.
        // TODO: Class Payment?
        public string PaymentMethod { get; set; }
        public DateTime ShellDateTime { get; set; }

        /// <summary>
        /// Definir si es una COMPRA (Purchase)
        /// o VENTA (Sale).
        /// Sin implementación.
        /// </summary>
        public string TransactionType { get; set; }
        
        // Foreign Key
        
        [ForeignKey("DocketId")]
        public uint WorkerId { get; set; }
        public Worker Worker { get; set; }
        
        [ForeignKey("ProductId")]
        public uint ProductId { get; set; }
        public Product Product { get; set; }
        
        
        
        
        
        
        
        
        
        
        
        // CRUD
        public static async Task<bool> ExistsInDbAsync(uint id)
        {
            await using (var dataBase = new SqLiteDbContext())
            {
                return dataBase.Transactions.Any(t => t.TransactionId == id);
            }
        } 
        
        public static async Task CreateInDbAsync(Transaction transaction)
        {
            await using (var dataBase = new SqLiteDbContext())
            {
                dataBase.Transactions.Add(transaction);
                await dataBase.SaveChangesAsync();
            }
        }
        
        public static async Task<Transaction> ReadFromDbById(uint targetId)
        {
            await using (var dataBase = new SqLiteDbContext())
            {
                if (await ExistsInDbAsync(targetId)) return dataBase.Transactions.First(t => t.TransactionId == targetId);
            }
            return new Transaction() { };
        }
        
        public static async Task<List<Transaction>> ReadAllFromDbAsync()
        {
            await using (var dataBase = new SqLiteDbContext())
            {
                return dataBase.Transactions.ToList();
            }
        }
        
    }
    
}