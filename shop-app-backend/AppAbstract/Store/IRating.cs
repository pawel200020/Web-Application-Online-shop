using Microsoft.AspNetCore.Identity;

namespace AppAbstract.Store;

public interface IRating
{
    public int Id { get;  }
    public int Rate { get;  }
    public int ProductId { get;  }
    public IProduct Product { get;  }
    public string UserId { get;  }
    public IdentityUser User { get;  }
}