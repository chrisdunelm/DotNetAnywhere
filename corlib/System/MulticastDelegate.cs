#if !LOCALTEST

namespace System {
	public abstract class MulticastDelegate : Delegate {

		protected override Delegate CombineImpl(Delegate follow) {

			MulticastDelegate ret = (MulticastDelegate)object.Clone(this);
			MulticastDelegate cur = ret;

			// Clone and add all the current delegate(s)
			for (MulticastDelegate del = (MulticastDelegate)this.pNext; del != null; del = (MulticastDelegate)del.pNext) {
				cur.pNext = (MulticastDelegate)object.Clone(del);
				cur = (MulticastDelegate)cur.pNext;
			}

			// Add all the following delegate(s)
			cur.pNext = (MulticastDelegate)object.Clone(follow);
			cur = (MulticastDelegate)cur.pNext;
			for (MulticastDelegate del = (MulticastDelegate)((MulticastDelegate)follow).pNext; del != null; del = (MulticastDelegate)del.pNext) {
				cur.pNext = (MulticastDelegate)object.Clone(del);
				cur = (MulticastDelegate)cur.pNext;
			}
			cur.pNext = null;

			return ret;
		}

		protected override Delegate RemoveImpl(Delegate d) {

			MulticastDelegate ret = null, cur = null;

			for (MulticastDelegate del = this; del != null; del = (MulticastDelegate)del.pNext) {
				// Miss out the one we're removing
				if (!del.Equals(d)) {
					if (ret == null) {
						ret = (MulticastDelegate)object.Clone(del);
						cur = ret;
					} else {
						cur.pNext = (MulticastDelegate)object.Clone(del);
						cur = (MulticastDelegate)cur.pNext;
					}
				}
			}
			if (cur != null) {
				cur.pNext = null;
			}

			return ret;
		}

	}
}

#endif