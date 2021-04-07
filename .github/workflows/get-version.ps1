$resp = Invoke-WebRequest -UseBasicParsing -Uri "https://api.github.com/repos/$($args[0])/releases" -Headers @{ "Authorization" = "token $($args[1])" }
$data = $resp.content | ConvertFrom-Json

$latest = If ($data.Length -gt 0) { $data[0].tag_name.split('.') | ForEach-Object { [int]::Parse($_) } } Else { @(0, 0, 0) }
$coming = @((Get-Date).Year, [int]::Parse((Get-Date).Month.ToString() + (Get-Date).Day.ToString().PadLeft(2, '0')), 0)

$match = $latest | ForEach-Object { $_ -eq $coming[[array]::IndexOf($latest, $_)] }
$major = If ($match[0]) { $latest[0] } Else { $coming[0] }
$minor = If ($match[1]) { $latest[1] } Else { $coming[1] }
$patch = If ($match[0] -and $match[1]) { $latest[2] + 1 } Else { 0 }

Write-Host "::set-output name=version::$($major).$($minor).$($patch)"
