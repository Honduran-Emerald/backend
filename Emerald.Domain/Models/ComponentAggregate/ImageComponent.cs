namespace Emerald.Domain.Models.ComponentAggregate
{
    public class ImageComponent : Component
    {
        public string? ImageId { get; set; }

        public ImageComponent(string? imageId)
        {
            ImageId = imageId;
        }
    }
}
