using Microsoft.AspNetCore.Mvc;
using SongStoreApi.Documents;
using SongStoreApi.Services.Abstractions;



[ApiController]
[Route("songs/v1.0")]
public class SongController : Controller
{
    private readonly ISongQueryServices _songServices;

    public SongController(ISongQueryServices songServices)
    {
        _songServices = songServices;
    }

    [HttpGet("song")]
    public ActionResult<HashSet<Song>> GetSongByName([FromQuery]string songName, CancellationToken cancellationToken)
    {
        var song = _songServices.GetSongsAsync(songName, cancellationToken);
        return Ok(song);
    }
}