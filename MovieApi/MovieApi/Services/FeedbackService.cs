using Microsoft.EntityFrameworkCore;
using MovieApi.DBContext;
using MovieApi.DTO.ResponseTO;
using MovieApi.Models;
using MovieApi.Services.Interface;
using System.Collections.Generic;

namespace MovieApi.Services
{
    public class FeedbackService(MovieServiceContext dbContext) : IFeedbackService
    {
        public async Task<bool> AddFeedback(int userId, int filmId, int mark, string text)
        {
            try
            {
                var feedback = new Feedback() { FbkUser = userId, FbkMark = mark, FbkText = text, FbkFilm = filmId };
                await dbContext.Feedbacks.AddAsync(feedback);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<FeedbackResponseTo[]> GetFeedbacks(int filmId)
        {
            var response = new List<FeedbackResponseTo>();
            try
            {
                var feedbacks = await dbContext.Feedbacks
                                         .Skip(0)
                                         .Take(20)
                                         .Where(f => f.FbkFilm == filmId)
                                         .ToListAsync();

                var userIds = feedbacks.Select(f => f.FbkUser).Distinct().ToList();

                var users = await dbContext.Users
                    .Where(u => userIds.Contains(u.UsrId))
                    .ToListAsync();

                for (int i = 0; i < feedbacks.Count; i++)
                {
                    response.Add(new FeedbackResponseTo()
                    {
                        Mark = feedbacks[i].FbkMark,
                        Text = feedbacks[i].FbkText,
                        UserName = users.FirstOrDefault(u=>u.UsrId == feedbacks[i].FbkUser)!.UsrName
                    });
                }

                return [.. response];

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
