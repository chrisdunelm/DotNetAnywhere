#if !LOCALTEST
namespace System.Collections {

	public interface IEnumerator {
		object Current { get;}
		bool MoveNext();
		void Reset();
	}

}
#endif
