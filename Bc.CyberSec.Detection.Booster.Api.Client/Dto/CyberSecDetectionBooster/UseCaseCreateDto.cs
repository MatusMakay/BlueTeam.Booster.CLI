﻿namespace Bc.CyberSec.Detection.Booster.Api.Client.Dto.SyslogNgConfigurator;

public class UseCaseCreateDto
{
    public List<string>? Mnemonics { get; set; }
    public string? KibanaRuleId { get; set; }
    public string? UseCaseIdentifier { get; set; }
    public string MitreAttackId { get; set; }
    public string? Name { get; set; }
}