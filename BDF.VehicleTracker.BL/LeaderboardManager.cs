using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDF.VehicleTracker.BL.Models;
using Microsoft.EntityFrameworkCore.Storage;
using BDF.VehicleTracker.PL;

namespace BDF.VehicleTracker.BL
{
    public static class LeaderboardManager
    {
        public async static Task<int> Insert(Leaderboard leaderBoard, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (VehicleEntities dc = new VehicleEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblLeaderboard newrow = new tblLeaderboard();
                    newrow.Id = Guid.NewGuid();
                    newrow.UserName = leaderBoard.UserName;
                    newrow.GameTime = DateTime.Now;
                    newrow.Score = leaderBoard.Score;

                    leaderBoard.Id = newrow.Id;

                    dc.tblLeaderboards.Add(newrow);
                    int results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    return results;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async static Task<IEnumerable<Leaderboard>> Load()
        {
            try
            {
                List<Leaderboard> leaderBoards = new List<Leaderboard>();
                using (VehicleEntities dc = new VehicleEntities())
                {
                    dc.tblLeaderboards
                        .OrderBy(l => l.Score)
                        .Take(15)
                        .ToList()
                        .ForEach(c => leaderBoards.Add(new Leaderboard
                        {
                            Id = c.Id,
                            UserName = c.UserName,
                            GameTime = c.GameTime,
                            Score = c.Score
                        }));
                }
                return leaderBoards;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async static Task<int> Delete(Guid id, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (VehicleEntities dc = new VehicleEntities())
                {

                    tblLeaderboard row = dc.tblLeaderboards.FirstOrDefault(c => c.Id == id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        dc.tblLeaderboards.Remove(row);
                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();

                    }
                    else
                    {
                        throw new Exception("Row was not found.");
                    }
                    return results;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async static Task<int> Update(Leaderboard leaderboard, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (VehicleEntities dc = new VehicleEntities())
                {
                    tblLeaderboard row = dc.tblLeaderboards.FirstOrDefault(c => c.Id == leaderboard.Id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();
                        row.UserName = leaderboard.UserName;
                        row.GameTime = leaderboard.GameTime;
                        row.Score = leaderboard.Score;

                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();

                    }
                    else
                    {
                        throw new Exception("Row was not found.");
                    }
                    return results;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
