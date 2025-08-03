import type { NextRequest } from 'next/server';
import { NextResponse } from 'next/server';


export function middleware(request: NextRequest) {
    const pathname = request.nextUrl.pathname;
    const cookie = request.cookies.get('testCookie');
    const isAdminRoute = pathname.startsWith('/admin');
    const isLoginPage = pathname === '/admin/login';

    if (isAdminRoute && !cookie && !isLoginPage) {
        return NextResponse.redirect(new URL('/admin/login', request.url));
    }

    return NextResponse.next();
}

export const config = {
    matcher: ['/admin/:path((?!login).*)'],
};