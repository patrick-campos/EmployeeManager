import { SideMenu } from "../side-menu";
import './style.scss';

interface IBaseLayoutProps {
  children: React.JSX.Element;
}

export default function BaseLayout(props:IBaseLayoutProps): React.JSX.Element {
  return(
     <div className="baselayout">
      <SideMenu/>
      <main className="baselayout__content">
        <div className="baselayout__inner">
          {props.children}
        </div>
      </main>
    </div>
  );
}