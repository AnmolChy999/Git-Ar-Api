using GitArApi.SongStoreApi.Contracts;
using GitArApi.SongStoreApi.Documents;
using Riok.Mapperly.Abstractions;

namespace GitArApi.SongStoreApi.Mapper;

[Mapper]
public partial class CreateSongRequestMapper
{
    public Song MapCreateSongRequestToSongDocument(CreateSongRequest request)
    {
        var mappedRequest = AutoMapCreateSongRequestToSongDocument(request);
        return mappedRequest;
    }

    [MapperIgnoreTarget(nameof(Song.Id))]
    public partial Song AutoMapCreateSongRequestToSongDocument(CreateSongRequest request);

    [MapperIgnoreTarget(nameof(Song.Id))]
    public partial Song AutoMapUpdateSongRequestToSongDocument(UpdateSongRequest request);
}
