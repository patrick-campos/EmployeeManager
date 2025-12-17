import { Users } from 'lucide-react';
import './logo.scss';

export interface SideMenuLogoProps {
  title: string;
  subtitle?: string;
}

export function SideMenuLogo(props:SideMenuLogoProps): React.JSX.Element {
    return (
    <div className="sidemenu-logo">
      <div className="sidemenu-logo__icon">
        <Users className="sidemenu-logo__icon-svg" />
      </div>
      <div className="sidemenu-logo__text">
        <h1 className="sidemenu-logo__title">{props.title}</h1>
        {props.subtitle && <p className="sidemenu-logo__subtitle">{props.subtitle}</p>}
      </div>
    </div>
    );


}