namespace Emerald.Application.Models
{
    public class UserModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string? Image { get; set; }

        public int Level { get; set; }
        public long Experience { get; set; }
        public int Glory { get; set; }

        public int QuestCount { get; set; }
        public int TrackerCount { get; set; }

        public UserModel(string userId, string userName, string? image, int level, long experience, int glory, int questCount, int trackerCount)
        {
            UserId = userId;
            UserName = userName;
            Image = image;
            Level = level;
            Experience = experience;
            Glory = glory;
            QuestCount = questCount;
            TrackerCount = trackerCount;
        }

        public UserModel()
        {
            UserId = "";
            UserName = "";
            Image = "";
            Level = 0;
            Experience = 0;
            Glory = 0;
            QuestCount = 0;
            TrackerCount = 0;
        }
    }
}
