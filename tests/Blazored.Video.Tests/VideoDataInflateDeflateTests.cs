﻿using Blazored.Video.Support;
using System;
using System.Text.Json;
using Xunit;

namespace Blazored.Video.Tests
{
	public class VideoDataInflateDeflateTests
	{
		JsonSerializerOptions options = new JsonSerializerOptions { IgnoreNullValues = true, PropertyNameCaseInsensitive = true };
		[Fact]
		public void CanBeInflatedFromNothing()
		{
			var data = "{}";

			var result = JsonSerializer.Deserialize<VideoEventData>(data, options);

			Assert.NotNull(result);
		}

		[Fact]
		public void CanBeInflatedFromEmptyData()
		{
			var data = @"{""name"":""Pause"",""state"":{}}";

			var result = JsonSerializer.Deserialize<VideoEventData>(data, options);

			Assert.NotNull(result);
			Assert.Equal("Pause", result.Name);
		}
		[Fact]
		public void CanBeInflatedFromAllData()
		{
			var data = @"{""name"":""Pause"",""state"":{""Autoplay"":false,""Controls"":true,""error"":{""code"":2,""message"":""some message""},
				""CurrentSrc"":""https://res.cloudinary.com/blazoredgitter/video/upload/v1557015491/samples/elephants.mp4"",
				""CurrentTime"":5.25375,""DefaultMuted"":false,""DefaultPlaybackRate"":1,""Duration"":48.516,""Ended"":false,
				""Loop"":false,""Muted"":false,""NetworkState"":1,""Paused"":true,""PlaybackRate"":1,""Played"":{},
				""Preload"":""metadata"",""ReadyState"":4,""Seekable"":{},""Seeking"":false,""Volume"":1}}";

			var result = JsonSerializer.Deserialize<VideoEventData>(data, options);

			Assert.NotNull(result);
			Assert.Equal("Pause", result.Name);
			Assert.False(result.State.Autoplay);
			Assert.True(result.State.Controls);
			Assert.Equal(new Uri("https://res.cloudinary.com/blazoredgitter/video/upload/v1557015491/samples/elephants.mp4"), result.State.CurrentSrc);
			Assert.Equal(5.25375, result.State.CurrentTime);
			Assert.False(result.State.DefaultMuted);
			Assert.Equal(1, result.State.DefaultPlaybackRate);
			Assert.Equal(48.516, result.State.Duration);
			Assert.False(result.State.Ended);
			Assert.False(result.State.Loop);
			Assert.False(result.State.Muted);
			Assert.Equal(NetworkState.NETWORK_IDLE, result.State.NetworkState);
			Assert.True(result.State.Paused);
			Assert.Equal(1, result.State.PlaybackRate);
			Assert.Equal(0, result.State.Played.Length);
			Assert.Equal("metadata", result.State.Preload);
			Assert.Equal(ReadyState.HAVE_ENOUGH_DATA, result.State.ReadyState);
			Assert.Equal(0, result.State.Seekable.Length);
			Assert.False(result.State.Seeking);
			Assert.Equal(1, result.State.Volume);
			Assert.NotNull(result.State.Error);
			Assert.Equal(MediaErrorType.MEDIA_ERR_NETWORK, result.State.Error.Code);
			Assert.Equal("some message", result.State.Error.Message);
		}

	}
}
