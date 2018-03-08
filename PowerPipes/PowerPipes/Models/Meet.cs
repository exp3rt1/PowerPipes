using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PowerPipes.Models
{
	public class Meet
	{
		public int Id { get; set; }

		public MeetHeader Header { get; set; } = new MeetHeader();

		public List<MeetResult> Results { get; set; } = new List<MeetResult>();

		public void CreateEmptyResults()
		{
			Results.Add(new MeetResult
			{
				Name = "Squat 1",
				Weight = 0,
				MovementType = 1,
				Success = false
			});

			Results.Add(new MeetResult
			{
				Name = "Squat 2",
				Weight = 0,
				MovementType = 1,
				Success = false
			});

			Results.Add(new MeetResult
			{
				Name = "Squat 3",
				Weight = 0,
				MovementType = 1,
				Success = false
			});

			Results.Add(new MeetResult
			{
				Name = "Bench 1",
				Weight = 0,
				MovementType = 2,
				Success = false
			});

			Results.Add(new MeetResult
			{
				Name = "Bench 2",
				Weight = 0,
				MovementType = 2,
				Success = false
			});

			Results.Add(new MeetResult
			{
				Name = "Bench 3",
				Weight = 0,
				MovementType = 2,
				Success = false
			});

			Results.Add(new MeetResult
			{
				Name = "Deadlift 1",
				Weight = 0,
				MovementType = 3,
				Success = false
			});

			Results.Add(new MeetResult
			{
				Name = "Deadlift 2",
				Weight = 0,
				MovementType = 3,
				Success = false
			});

			Results.Add(new MeetResult
			{
				Name = "Deadlift 3",
				Weight = 0,
				MovementType = 3,
				Success = false
			});
		}
	}
}