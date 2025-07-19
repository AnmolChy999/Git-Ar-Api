namespace GitArApi.SongStoreApi.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GitArApi.SongStoreApi.Documents;
using GitArApi.SongStoreApi.Services.Query.Abstractions;
using GitArApi.SongStoreApi.Services.Command.Abstractions;
using GitArApi.SongStoreApi.Contracts;
using Microsoft.AspNetCore.Mvc.Infrastructure;

[ApiController]
[Route("songs/v1.0")]
public class SongController : Controller
{
    private readonly ISongQueryServices _songServices;

    private readonly ISongCommandServices _commandServices;

    public SongController(ISongQueryServices songServices, ISongCommandServices commandServices)
    {
        _songServices = songServices;
        _commandServices = commandServices;
    }

    [HttpGet("song")]
    public ActionResult<HashSet<Song>> GetSongByName([FromQuery] string songName, CancellationToken cancellationToken)
    {
        var song = _songServices.GetSongsAsync(songName, cancellationToken);
        return Ok(song);
    }

    [HttpPost("song")]
    public async Task<IActionResult> AddSongAsync([FromBody] CreateSongRequest request, CancellationToken cancellationToken)
    {
        await _commandServices.AddSongAsync(request, cancellationToken);
        return Ok(new { message = "Song added successfully" });
    }

    [HttpPut("song")]
    public async Task<IActionResult> UpdateSongAsync([FromQuery] string id, [FromBody] UpdateSongRequest request, CancellationToken cancellationToken)
    {
        await _commandServices.UpdateSongAsync(id, request, cancellationToken);
        return Ok(new { message = "Song updated successfully" });
    }

    [HttpDelete("song")]
    public async Task<IActionResult> DeleteSongAsync([FromQuery]string id, CancellationToken cancellationToken)
    {
        await _commandServices.DeleteSongAsync(id, cancellationToken);
        return Ok(new { message = "Song has been deleted successfully." });
    }
}