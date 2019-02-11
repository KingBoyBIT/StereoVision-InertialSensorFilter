clear,clc,close all

dirs = dir;

% filepath

syms f x1 x2
f=(1/2)*x1^2+x2^2-2*x1*x2;
x=[2;1];
a=[1 0;0 2];% A
g1=diff(f,x1);
g2=diff(f,x2);
g=[g1;g2];%导数
 
%            x1
g11=subs(g1,{x1,x2},{x(1) x(2)});
g22=subs(g2,{x1,x2},{x(1) x(2)});
g=[g11;g22]
d=-g;
d11=subs(d(1),{x1,x2},{x(1) x(2)});
d22=subs(d(2),{x1,x2},{x(1) x(2)});
d=[d11;d22]
af=(-g'*d)/(d'*a*d)
x=x+af*d
g11=subs(g1,{x1,x2},{x(1) x(2)});
g22=subs(g2,{x1,x2},{x(1) x(2)});
g=[g11;g22]
%             x1
 
e=0.1;%精度
%共轭梯度
for i=1:100
    if g==0
        disp('x')
        x
        break
    else
         
    
        b=(g'*a*d)/(d'*a*d);%β
        d=-g+b*d;
         
        d11=subs(d(1),{x1,x2},{x(1) x(2)});
        d22=subs(d(2),{x1,x2},{x(1) x(2)});
        d=[d11;d22];
         
        af=(-g'*d)/(d'*a*d);%步长α
         
        x=x+af*d
         
        g11=subs(g1,{x1,x2},{x(1) x(2)});
        g22=subs(g2,{x1,x2},{x(1) x(2)});
        g=[g11;g22];
    end
end
