using Microsoft.AspNetCore.Mvc;
using SongStoreApi.Documents;
using SongStoreApi.Services.Abstractions;


[Route("songs/v1.0")]
public class SongController : Controller
{
    private readonly ISongServices _songServices;

    public SongController(ISongServices songServices)
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