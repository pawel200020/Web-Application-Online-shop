"use client";
import Link from 'next/link';
import ThemeSwitcher from "@/app/context/ThemeSwitcher";
import {ThemeContext} from "@/app/context/ThemeContext";
import {useContext} from "react";

export function Navbar(){
    const { changeTheme } = useContext(ThemeContext);
    return(<>
        <div className="navbar bg-base-300 rounded-box shadow-xl">
            <div className="navbar-start">
                <div className="dropdown">
                    <div tabIndex={0} role="button" className="btn btn-ghost lg:hidden">
                        <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" fill="none" viewBox="0 0 24 24"
                             stroke="currentColor">
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2"
                                  d="M4 6h16M4 12h8m-8 6h16"/>
                        </svg>
                    </div>
                    <ul tabIndex={1}
                        className="menu menu-sm dropdown-content mt-3 z-[1] p-2 shadow bg-base-100 rounded-box w-52">
                        <li><Link href="/public" className='link'>Main page</Link></li>
                        <li>
                            <a>Parent</a>
                            <ul className="p-2">
                                <li><a>Submenu 1</a></li>
                                <li><a>Submenu 2</a></li>
                            </ul>
                        </li>
                        <li><a>Item 3</a></li>
                    </ul>
                </div>
                <Link className="btn btn-ghost text-xl" href={"/public"}>KKMK</Link>
            </div>
            <div className="navbar-center hidden lg:flex">
                <ul className="menu menu-horizontal px-1">
                    <li><Link href={"/public"}>Item 1</Link></li>
                    <li>
                        <details>
                            <summary>Parent</summary>
                            <ul className="p-2">
                                <li><a>Submenu 1</a></li>
                                <li><a>Submenu 2</a></li>
                            </ul>
                        </details>
                    </li>
                    <li><a>Item 3</a></li>
                </ul>
            </div>

            <div className="navbar-end">

                <ThemeSwitcher handleOnClick={changeTheme}/>
                <div className="dropdown dropdown-end">
                    <div tabIndex={0} role="button" className="btn btn-ghost     avatar">
                       Test
                    </div>
                    <ul
                        tabIndex={0}
                        className="menu dropdown-content bg-base-100 rounded-box z-[1] w-52 p-2 shadow">
                        <li><a>Settings</a></li>
                        <li><a>Logout</a></li>
                    </ul>
                </div>
                <div className="dropdown">
                    <div tabIndex="0" role="button" className="btn">Language</div>
                    <ul tabIndex="0" data-dropdown-toggle="dropdownHover"
                        className="dropdown-content menu bg-base-100 rounded-box z-[1] w-52 p-2 shadow">
                        <li><a>EN</a></li>
                        <li><a>PL</a></li>
                        <li><a>DE</a></li>
                    </ul>
                </div>
                <div className="flex-none">
                    <ul className="menu menu-horizontal px-1">
                        <li><Link href={"/src/app/%5Blocale%5D/login/"}>Login</Link></li>
                        <li><Link href={"/src/app/%5Blocale%5D/register/"}>Register</Link></li>
                    </ul>
                </div>
            </div>
        </div>
    </>)
}