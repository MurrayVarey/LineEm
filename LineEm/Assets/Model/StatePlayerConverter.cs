using NoughtsAndCrosses;

namespace NoughtsAndCrosses
{
	public class StatePlayerConverter
	{
		public StatePlayerConverter ()
		{
		}

		public eState GetPlayerState(int player)
		{
			return player == 0 ? eState.nought : eState.cross;
		}

	}
}

