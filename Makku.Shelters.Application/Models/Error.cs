using Makku.Shelters.Application.Enums;

namespace Makku.Shelters.Application.Models;

public class Error
{
    public ErrorCode Code { get; set; }
    public string Message { get; set; }
}