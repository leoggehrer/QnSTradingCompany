//@QnSGeneratedCode
import { Identity } from '@app-contracts/persistence/account/identity';
/** CustomImportBegin **/
/** CustomImportEnd **/
export interface LoginSession
{
    id: number;
    identityId: number;
    isRemoteAuth: boolean;
    origin: string;
    name: string;
    email: string;
    timeOutInMinutes: number;
    jsonWebToken: string;
    sessionToken: string;
    loginTime: Date;
    lastAccess: Date;
    logoutTime: Date;
    optionalInfo: string;
    identity: Identity;
/** CustomCodeBegin **/
/** CustomCodeEnd **/
}
