using System.Text.Json.Serialization;
using AvaloniaExampleProject.Models;

namespace AvaloniaExampleProject;

[JsonSerializable(typeof(MainConfig))]
public sealed partial class JsonContext : JsonSerializerContext;
