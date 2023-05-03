namespace Boardgames.DataProcessor
{
    using Boardgames.Data;
    using Boardgames.DataProcessor.ExportDto;
    using Boardgames.Utilities;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    public class Serializer
    {
        private static XmlHelper xmlHelper;
        public static string ExportCreatorsWithTheirBoardgames(BoardgamesContext context)
        {
            //task 3 part 2
            xmlHelper = new XmlHelper();

            ExportCreatorDto[] creators = context
                    .Creators
                    .Where(c => c.Boardgames.Any())
                    .Select(c => new ExportCreatorDto()
                    {
                        CreatorName = c.FirstName + ' ' + c.LastName,
                        BoardgamesCount = c.Boardgames.Count,
                        Boardgames = c.Boardgames
                            .Select(b => new ExportBoardgameDto()
                            {
                                BoardgameName = b.Name,
                                BoardgameYearPublished = b.YearPublished
                            })
                            .OrderBy(b => b.BoardgameName)
                            .ToArray()
                    })
                    .OrderByDescending(c => c.BoardgamesCount)
                    .ThenBy(c => c.CreatorName)
                    .ToArray();

            return xmlHelper.Serialize(creators, "Creators");
        }

        public static string ExportSellersWithMostBoardgames(BoardgamesContext context, int year, double rating)
        {
            //task 3 part 1 json export
            var sellers = context.Sellers
                .Include(s => s.BoardgamesSellers)
                .ThenInclude(bs => bs.Boardgame)
                .AsNoTracking()
                .ToArray()
                .Where(s => s.BoardgamesSellers.Any(bs => bs.Boardgame.YearPublished >= year
                                                              && bs.Boardgame.Rating <= rating))
                .Select(s => new
                {
                    s.Name,
                    s.Website,
                    Boardgames = s.BoardgamesSellers
                    .Where(bs => bs.Boardgame.YearPublished >= year && bs.Boardgame.Rating <= rating)
                    .Select(bs => new
                    {
                        Name = bs.Boardgame.Name,
                        Rating = bs.Boardgame.Rating,
                        Mechanics = bs.Boardgame.Mechanics,
                        Category = bs.Boardgame.CategoryType.ToString()
                    })
                    .OrderByDescending(g => g.Rating)
                    .ThenBy(g => g.Name)
                    .ToArray()
                })
                //Length? or Count
                .OrderByDescending(s => s.Boardgames.Length)
                .ThenBy(s => s.Name)
                .Take(5)
                .ToArray();

            return JsonConvert.SerializeObject(sellers, Formatting.Indented);
        }
    }
}