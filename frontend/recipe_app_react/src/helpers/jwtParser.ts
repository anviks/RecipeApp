export function parseJwt(token: string): any {
    const base64Url = token.split('.')[1];
    if (!base64Url) {
        return null;
    }
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));
    const parsedPayload = JSON.parse(jsonPayload);

    return {
        username: parsedPayload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
        email: parsedPayload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'],
        expiresAt: parsedPayload.exp
    };
}