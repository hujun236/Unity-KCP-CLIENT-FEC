#include "sess.h"
#ifdef _WIN32
int gettimeofday(struct timeval *tp, void *tzp);
#define DLL_EXPORT _declspec(dllexport)
extern "C" {
#else
#define DLL_EXPORT extern "C" __attribute__ ((visibility("default")))
#endif

#define SESSION_PTR int*



void
itimeofday(long *sec, long *usec) {
	struct timeval time;
	gettimeofday(&time, NULL);
	if (sec) *sec = time.tv_sec;
	if (usec) *usec = time.tv_usec;
}

IUINT64 iclock64(void) {
	long s, u;
	IUINT64 value;
	itimeofday(&s, &u);
	value = ((IUINT64)s) * 1000 + (u / 1000);
	return value;
}

IUINT32 iclock() {
	return (IUINT32)(iclock64() & 0xfffffffful);
}








//export:
DLL_EXPORT void KCPReset(SESSION_PTR sess)
{
	UDPSession* _sess = (UDPSession*)sess;
	UDPSession::Destroy(_sess);
	
}
DLL_EXPORT void KCPSetNoDelay(SESSION_PTR sess, int nodelay, int interval, int resend, int nc)
{
	UDPSession* _sess = (UDPSession*)sess;
	_sess->NoDelay(nodelay, interval, resend, nc);
}
DLL_EXPORT void KCPSetWndSize(SESSION_PTR sess, int sndwnd, int rcvwnd)
{
	UDPSession* _sess = (UDPSession*)sess;
	_sess->WndSize(sndwnd, rcvwnd);
}
DLL_EXPORT void KCPSetMtu(SESSION_PTR sess, int mtu)
{
	UDPSession* _sess = (UDPSession*)sess;
	_sess->SetMtu(mtu);
}

DLL_EXPORT void KCPSetStreamMode(SESSION_PTR sess, bool stream)
{
	UDPSession* _sess = (UDPSession*)sess;
	_sess->SetStreamMode(stream);
}
DLL_EXPORT void KCPSetDSCP(SESSION_PTR sess, int dscp)
{
	UDPSession* _sess = (UDPSession*)sess;
	_sess->SetDSCP(dscp);
}

DLL_EXPORT SESSION_PTR KCPDial(const char* ip, unsigned short port, int dataShards, int parityShards)
{
	UDPSession* _sess = UDPSession::DialWithOptions(ip, port, size_t(dataShards), size_t(parityShards));
	if (!_sess)
		return 0;
	return (SESSION_PTR)_sess;
}
DLL_EXPORT int KCPWrite(SESSION_PTR sess, const char* buff, int size)
{
	UDPSession* _sess = (UDPSession*)sess;

	return _sess->Write(buff, size);

	
}
DLL_EXPORT int KCPRead(SESSION_PTR sess, char* buff, int size)
{
	
	UDPSession* _sess = (UDPSession*)sess;

	return _sess->Read(buff, size);
}
DLL_EXPORT void KCPUpdate(SESSION_PTR sess)
{
	UDPSession* _sess = (UDPSession*)sess;
	_sess->Update(iclock());
	
}



#ifdef _WIN32
}
#endif