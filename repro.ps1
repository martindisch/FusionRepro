echo "T0: Doing the first request"
curl.exe -w "\n" http://localhost:5122/hello/martin

Start-Sleep -Seconds 3

echo "T3: Doing the second request, will receive cached value and start eager refresh that doesn't complete and free the lock"
curl.exe -w "\n" http://localhost:5122/hello/martin

Start-Sleep -Seconds 3

echo "T6: Doing the third request, will receive cached value because it thinks refresh is still ongoing"
curl.exe -w "\n" http://localhost:5122/hello/martin

Start-Sleep -Seconds 7

echo "T12: Doing the fourth request, cache is expired but factory can't run because of deadlock"
curl.exe -w "\n" http://localhost:5122/hello/martin
