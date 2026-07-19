
namespace TimeSystems
{
	public interface ITimer
	{
		float Elapsed {
			get;
		}

		void Reset();
	}
}
