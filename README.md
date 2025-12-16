# Simple video player as second window

This project is for demonstration purposes only.

>Status: updating ♻️ <br>
>Version: not applicable<br>
>Date released: 12/13/2025

## What does it do? - Purpose
Opens a window what contains a little video player. Accepts few formats.

## Content
1. WPF without dependencies
2. Volume slider - responsive
3. Mute button
4. Duration slider - require optimization
5. Play button
6. Pause button
7. Stop button
8. Open button - videos

## How does it work? - Behavior

* Layout has a grid (8 rows, 2 cols).
* Can be resized.

**Mute button - self-explanatory**

**Volume slider:**
  * Disabled by default when there is not video;
  * Changes between 0-100;
  * Can be selected in any point.

**Duration slider:**
  * Disabled by default when there is not video;
  * Changes between 0 to `video-width` or `NaturalDuration.TimeSpan`;
  * _Cannot be selected in any point._ (WIP)

**Play button:**
  * Disabled by default when there is not video;
  * Disabled when clicked once. Enabled when pause or stop button is clicked.

**Pause button:**
  * Disabled by default when there is not video;
  * Disabled when clicked once. Enabled when play button is clicked.

**Stop button:**
  * Disabled by default when there is not video;
  * Disabled when clicked once. Enabled when play or pause button is clicked.

**Open button:**
  * Open in following formats: wmv, avi, mp4, mpeg and mov
  * `openFile.Filter = "Vídeos|*.wmv;*.avi;*.mp4;*.mpeg;*.mov";`



## What was used? - Resources
GUI          | .NET  | SQL | Web
------------ | ----- | --- | ---
WPF          | v.8.0 | No  | No
