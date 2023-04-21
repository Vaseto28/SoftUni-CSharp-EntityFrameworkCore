namespace MusicHub
{
    using System;
    using System.Text;
    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main()
        {
            MusicHubDbContext context =
                new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);

            Console.WriteLine(ExportSongsAboveDuration(context, 4));
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var albums = context.Albums
                .Where(a => a.ProducerId == producerId)
                .OrderByDescending(a => a.Price)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var a in albums)
            {
                sb.AppendLine($"-AlbumName: {a.Name}");
                sb.AppendLine($"-ReleaseDate: {a.ReleaseDate.ToString("MM/dd/yyyy")}");
                sb.AppendLine($"-ProducerName: {a.Producer.Name}");

                if (a.Songs.Count != 0)
                {
                    sb.AppendLine("-Songs:");
                    int songNumber = 1;
                    foreach (var s in a.Songs.OrderByDescending(x => x.Name).ThenBy(x => x.Writer.Name))
                    {
                        sb.AppendLine($"---#{songNumber}");
                        sb.AppendLine($"---SongName: {s.Name}");
                        sb.AppendLine($"---Price: {s.Price:f2}");
                        sb.AppendLine($"---Writer: {s.Writer.Name}");

                        songNumber++;
                    }
                }

                sb.AppendLine($"-AlbumPrice: {a.Price:f2}");
            }

            return sb.ToString().Trim();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var songs = context.Songs
                .AsEnumerable()
                .Where(x => x.Duration.TotalSeconds > duration)
                .Select(x => new
                {
                    x.Name,
                    WriterName = x.Writer.Name,
                    PerformerNames = x.SongPerformers
                        .Select(sp => $"{sp.Performer.FirstName} {sp.Performer.LastName}")
                        .OrderBy(x => x).ToList(),
                    AlbumProducer = x.Album.Producer.Name,
                    x.Duration
                })
                .OrderBy(x => x.Name)
                .ThenBy(x => x.WriterName);

            StringBuilder sb = new StringBuilder();
            int songCounter = 1;
            foreach (var s in songs)
            {
                sb.AppendLine($"-Song #{songCounter}");
                sb.AppendLine($"---SongName: {s.Name}");
                sb.AppendLine($"---Writer: {s.WriterName}");

                if (s.PerformerNames.Count != 0)
                {
                    foreach (var p in s.PerformerNames)
                    {
                        sb.AppendLine($"---Performer: {p}");
                    }
                }

                sb.AppendLine($"---AlbumProducer: {s.AlbumProducer}");
                sb.AppendLine($"---Duration: {s.Duration.ToString("c")}");

                songCounter++;
            }

            return sb.ToString().Trim();
        }
    }
}
