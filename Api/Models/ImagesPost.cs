using Infraestrutura.Models;

namespace Api.Models;

public class ImagesPost : BaseModel
{
    public string Image { get; private set; }

    public Guid PostId { get; private set; }

    public virtual Post Post { get; private set; }
}
