import { SideMenuItem, type SideMenuItemProps } from "./item";
import './navbar.scss';

export interface sidemenuNavProps {
  items: SideMenuItemProps[];
}

export function SideMenuNav(props:sidemenuNavProps): React.JSX.Element {
    return (
    <nav className="sidemenu-nav">
      <ul className="sidemenu-nav__list">
        {props.items.map((item) => (
          <SideMenuItem key={item.href} {...item} />
        ))}
      </ul>
    </nav>
    );


}