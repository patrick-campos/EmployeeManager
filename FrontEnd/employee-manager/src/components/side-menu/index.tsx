import { Users, UserPlus, List } from 'lucide-react';
import { SideMenuFooter } from "./footer";
import { SideMenuLogo } from "./logo";
import { SideMenuNav } from "./navbar";
import type { SideMenuItemProps } from './item';
import './index.scss';

export function SideMenu(): React.JSX.Element {

    const navItems: SideMenuItemProps[] = [
        { title: 'List Employees', href: '/', icon: List },
        { title: 'Cadastrar', href: '/cadastrar', icon: UserPlus },
        { title: 'Colaboradores', href: '/colaboradores', icon: Users },
    ];

    return (
        <aside className="sidemenu">
            <div className="sidemenu__header">
                <SideMenuLogo title="HR Manager" subtitle="People Management" />
            </div>
            <SideMenuNav items={navItems} />
            <SideMenuFooter
                description="System of management of employees."
                version="v1.0.0"
            />
        </aside>
    );
}