import { SettingsDto } from "./"

export interface AuthRegisterRequest {
   Settings: SettingsDto;
   Password: string;
   PasswordConfirm: string
}