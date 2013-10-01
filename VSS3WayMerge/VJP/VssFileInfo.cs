using System.Collections.Generic;

namespace Vss3WayMerge.VJP
{
	public class VssNodeInfo
	{
		public string Spec;

		public List<VssNodeChange> Changes = new List<VssNodeChange>();

		public bool IsModified;

		public bool IsRolledback;
		public bool IsArchived;
		public bool IsAdded;
		public bool IsBranched;
		public bool IsDeleted;
		// has sense only for moved/renamed files. Old spec will be marked with IsDelete and IsDeletedByMove
		public bool IsDeletedByMove;
		// only if file was recovered without previously deletion
		public bool IsRecovered;

		public string MovedFrom;
		public string SharedFrom;
		public string CopiedFrom;

		public int FirstChange;
		public int LatestVersion;

		public bool IsPureModified
		{
			get
			{
				return IsModified && !IsRolledback && !IsAdded && !IsArchived && !IsDeleted && !IsRecovered && MovedFrom == null && SharedFrom == null && CopiedFrom == null;
			}
		}

		public bool IsPureAdded
		{
			get
			{
				return IsAdded && !IsDeleted && MovedFrom == null && SharedFrom == null && CopiedFrom == null;
			}
		}
	}
}
