namespace Vss3WayMerge.Interfaces
{
	interface IErrorNotification
	{
		void NotifyError(string message, string spec, string hint);
	}
}
