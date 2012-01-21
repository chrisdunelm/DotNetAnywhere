#if !LOCALTEST

namespace System {
	public delegate TOutput Converter<TInput, TOutput>(TInput input);
}

#endif
