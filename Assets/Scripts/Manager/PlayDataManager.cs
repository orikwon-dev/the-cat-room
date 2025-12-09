using UnityEngine;
using MyCat.Domain;

namespace MyCat.Runtime
{
    public class PlayDataManager : MonoSingletonBase<PlayDataManager>
    {
        private PlayData _playData = new PlayData();
        public PlayData CurrentPlayData => _playData;
    }
}