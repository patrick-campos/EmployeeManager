import { SideMenuItem, type SideMenuItemProps } from "./item";
import './navbar.scss';

export interface SideMenuFooterProps {
  description: string;
  version?: string;
}

export function SideMenuFooter(props:SideMenuFooterProps): React.JSX.Element {
    return (
    <div className="sidemenu-footer">
      <div className="sidemenu-footer__content">
        <p className="sidemenu-footer__description">{props.description}</p>
        {props.version && <p className="sidemenu-footer__version">{props.version}</p>}
      </div>
    </div>
    );


}