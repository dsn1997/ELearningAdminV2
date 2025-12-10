namespace IIG.Core.Common.Models.Files;

public class AuthTokenVngDto
{
    public Token Token { get; set; }
}

public class Catalog
{
    public List<Endpoint> Endpoints { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
}

public class Domain
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class Endpoint
{
    public string Interface { get; set; }
    public string Url { get; set; }
    public string Region { get; set; }
}

public class Project
{
    public Domain Domain { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
}

public class Role
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class Token
{
    public List<Catalog> Catalog { get; set; }
    public bool IsDomain { get; set; }
    public List<string> Methods { get; set; }
    public List<Role> Roles { get; set; }
    public DateTime ExpiresAt { get; set; }
    public Project Project { get; set; }
    
    public User User { get; set; }
    public List<string> AuditIds { get; set; }
    public DateTime IssuedAt { get; set; }
}

public class User
{
    public Domain Domain { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
}