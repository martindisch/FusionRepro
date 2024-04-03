# FusionRepro

Reproduction for a deadlock issue.

Open two terminals:

1. `dotnet run`
2. `./repro`

You should see something like this

```
T0: Doing the first request
{"text":"Hello, martin!"}
T3: Doing the second request, will receive cached value and start eager refresh that doesn't complete and free the lock
{"text":"Hello, martin!"}
T6: Doing the third request, will receive cached value because it thinks refresh is still ongoing
{"text":"Hello, martin!"}
T12: Doing the fourth request, cache is expired but factory can't run because of deadlock
```

```
[17:40:28] dbug: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIM K=martin): calling GetOrSetAsync<T> (null)
[17:40:28] trce: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIM K=martin): [MC] trying to get from memory
[17:40:28] dbug: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIM K=martin): [MC] memory entry not found
[17:40:28] trce: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIM K=martin): waiting to acquire the LOCK
[17:40:28] trce: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIM K=martin): LOCK acquired
[17:40:28] trce: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIM K=martin): [MC] trying to get from memory
[17:40:28] dbug: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIM K=martin): [MC] memory entry not found
[17:40:28] dbug: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIM K=martin): calling the factory (timeout=/)
[17:40:28] info: Greeting[0]
      Executing factory for martin
[17:40:28] info: Greeting[0]
      Finishing factory for martin
[17:40:28] dbug: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIM K=martin): [MC] saving entry in memory FE[FFS=N, LEXP=10s, EEXP=998ms, LM=/, ET=/]
[17:40:28] trce: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIM K=martin): releasing MEMORY LOCK
[17:40:28] trce: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIM K=martin): MEMORY LOCK released
[17:40:28] dbug: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIM K=martin): return FE[FFS=N, LEXP=10s, EEXP=997ms, LM=/, ET=/]
[17:40:31] dbug: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIN K=martin): calling GetOrSetAsync<T> (null)
[17:40:31] trce: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIN K=martin): [MC] trying to get from memory
[17:40:31] dbug: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIN K=martin): [MC] memory entry found FE[FFS=N, LEXP=7s, EEXP=-2s, LM=/, ET=/]
[17:40:31] trce: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIN K=martin): should eagerly refresh
[17:40:31] trce: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIN K=martin): waiting to acquire the LOCK
[17:40:31] trce: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIN K=martin): LOCK acquired
[17:40:31] trce: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIN K=martin): using memory entry
[17:40:31] dbug: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIN K=martin): return FE[FFS=N, LEXP=7s, EEXP=-2s, LM=/, ET=/]
[17:40:31] trce: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIN K=martin): eagerly refreshing
[17:40:31] info: Greeting[0]
      Executing factory for martin
[17:40:31] dbug: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIN K=martin): trying to complete a background factory
[17:40:34] dbug: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIO K=martin): calling GetOrSetAsync<T> (null)
[17:40:34] trce: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIO K=martin): [MC] trying to get from memory
[17:40:34] dbug: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIO K=martin): [MC] memory entry found FE[FFS=N, LEXP=4s, EEXP=-5s, LM=/, ET=/]
[17:40:34] trce: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIO K=martin): should eagerly refresh
[17:40:34] trce: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIO K=martin): waiting to acquire the LOCK
[17:40:34] trce: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIO K=martin): LOCK timeout
[17:40:34] trce: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIO K=martin): eager refresh already occurring
[17:40:34] trce: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIO K=martin): using memory entry
[17:40:34] dbug: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIO K=martin): return FE[FFS=N, LEXP=4s, EEXP=-5s, LM=/, ET=/]
[17:40:41] dbug: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIP K=martin): calling GetOrSetAsync<T> (null)
[17:40:41] trce: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIP K=martin): [MC] trying to get from memory
[17:40:41] dbug: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIP K=martin): [MC] memory entry not found
[17:40:41] trce: ZiggyCreatures.Caching.Fusion.FusionCache[0]
      FUSION [N=FusionCache I=8208c6d6788446258bbbd87de0993477] (O=0HN2JUHHP9HIP K=martin): waiting to acquire the LOCK
```

## License

[MIT license](LICENSE)
