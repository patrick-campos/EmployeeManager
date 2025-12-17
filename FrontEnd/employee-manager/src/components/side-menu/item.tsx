import { Link, useLocation } from 'react-router-dom';
import type { LucideIcon, IconNode } from "lucide-react";
import './item.scss';

export interface SideMenuItemProps {
    title: string;
    href: string;
    icon: LucideIcon;
}

export function SideMenuItem(props: SideMenuItemProps): React.JSX.Element {
    const location = useLocation();
    const isActive = location.pathname === props.href;

    return (
        <li className="sidemenu-item">
            <Link
                to={props.href}
                className={`sidemenu-item__link ${isActive ? 'sidemenu-item__link--active' : ''}`}>
                <props.icon className="sidemenu-item__icon" />
                <span className="sidemenu-item__title">{props.title}</span>
            </Link>
        </li>
    );


}