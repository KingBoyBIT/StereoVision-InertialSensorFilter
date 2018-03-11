/********************************************************************************
** Form generated from reading UI file 'QtGuiVisionPrj.ui'
**
** Created by: Qt User Interface Compiler version 5.9.0
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_QTGUIVISIONPRJ_H
#define UI_QTGUIVISIONPRJ_H

#include <QtCore/QVariant>
#include <QtWidgets/QAction>
#include <QtWidgets/QApplication>
#include <QtWidgets/QButtonGroup>
#include <QtWidgets/QGridLayout>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QLabel>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QTextEdit>
#include <QtWidgets/QToolBar>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_QtGuiVisionPrjClass
{
public:
    QWidget *centralWidget;
    QGridLayout *gridLayout_4;
    QGridLayout *gridLayout_2;
    QGridLayout *gridLayout_3;
    QPushButton *CamshotBtn;
    QPushButton *OpenCamBtn;
    QPushButton *CloseCamBtn;
    QTextEdit *textEditin;
    QGridLayout *gridLayout;
    QLabel *campicblack;
    QLabel *campicwhite;
    QLabel *campicwhiteshot;
    QLabel *campicblackshot;
    QMenuBar *menuBar;
    QToolBar *mainToolBar;
    QStatusBar *statusBar;

    void setupUi(QMainWindow *QtGuiVisionPrjClass)
    {
        if (QtGuiVisionPrjClass->objectName().isEmpty())
            QtGuiVisionPrjClass->setObjectName(QStringLiteral("QtGuiVisionPrjClass"));
        QtGuiVisionPrjClass->resize(797, 534);
        centralWidget = new QWidget(QtGuiVisionPrjClass);
        centralWidget->setObjectName(QStringLiteral("centralWidget"));
        gridLayout_4 = new QGridLayout(centralWidget);
        gridLayout_4->setSpacing(6);
        gridLayout_4->setContentsMargins(11, 11, 11, 11);
        gridLayout_4->setObjectName(QStringLiteral("gridLayout_4"));
        gridLayout_2 = new QGridLayout();
        gridLayout_2->setSpacing(6);
        gridLayout_2->setObjectName(QStringLiteral("gridLayout_2"));
        gridLayout_3 = new QGridLayout();
        gridLayout_3->setSpacing(6);
        gridLayout_3->setObjectName(QStringLiteral("gridLayout_3"));
        CamshotBtn = new QPushButton(centralWidget);
        CamshotBtn->setObjectName(QStringLiteral("CamshotBtn"));

        gridLayout_3->addWidget(CamshotBtn, 0, 0, 1, 1);

        OpenCamBtn = new QPushButton(centralWidget);
        OpenCamBtn->setObjectName(QStringLiteral("OpenCamBtn"));

        gridLayout_3->addWidget(OpenCamBtn, 0, 1, 1, 1);

        CloseCamBtn = new QPushButton(centralWidget);
        CloseCamBtn->setObjectName(QStringLiteral("CloseCamBtn"));

        gridLayout_3->addWidget(CloseCamBtn, 0, 2, 1, 1);

        textEditin = new QTextEdit(centralWidget);
        textEditin->setObjectName(QStringLiteral("textEditin"));

        gridLayout_3->addWidget(textEditin, 1, 0, 1, 3);


        gridLayout_2->addLayout(gridLayout_3, 0, 0, 1, 1);

        gridLayout = new QGridLayout();
        gridLayout->setSpacing(6);
        gridLayout->setObjectName(QStringLiteral("gridLayout"));
        campicblack = new QLabel(centralWidget);
        campicblack->setObjectName(QStringLiteral("campicblack"));

        gridLayout->addWidget(campicblack, 1, 1, 1, 1);

        campicwhite = new QLabel(centralWidget);
        campicwhite->setObjectName(QStringLiteral("campicwhite"));

        gridLayout->addWidget(campicwhite, 1, 0, 1, 1);

        campicwhiteshot = new QLabel(centralWidget);
        campicwhiteshot->setObjectName(QStringLiteral("campicwhiteshot"));

        gridLayout->addWidget(campicwhiteshot, 2, 0, 1, 1);

        campicblackshot = new QLabel(centralWidget);
        campicblackshot->setObjectName(QStringLiteral("campicblackshot"));

        gridLayout->addWidget(campicblackshot, 2, 1, 1, 1);


        gridLayout_2->addLayout(gridLayout, 1, 0, 1, 1);


        gridLayout_4->addLayout(gridLayout_2, 0, 0, 1, 1);

        QtGuiVisionPrjClass->setCentralWidget(centralWidget);
        menuBar = new QMenuBar(QtGuiVisionPrjClass);
        menuBar->setObjectName(QStringLiteral("menuBar"));
        menuBar->setGeometry(QRect(0, 0, 797, 23));
        QtGuiVisionPrjClass->setMenuBar(menuBar);
        mainToolBar = new QToolBar(QtGuiVisionPrjClass);
        mainToolBar->setObjectName(QStringLiteral("mainToolBar"));
        QtGuiVisionPrjClass->addToolBar(Qt::TopToolBarArea, mainToolBar);
        statusBar = new QStatusBar(QtGuiVisionPrjClass);
        statusBar->setObjectName(QStringLiteral("statusBar"));
        QtGuiVisionPrjClass->setStatusBar(statusBar);

        retranslateUi(QtGuiVisionPrjClass);

        QMetaObject::connectSlotsByName(QtGuiVisionPrjClass);
    } // setupUi

    void retranslateUi(QMainWindow *QtGuiVisionPrjClass)
    {
        QtGuiVisionPrjClass->setWindowTitle(QApplication::translate("QtGuiVisionPrjClass", "QtGuiVisionPrj", Q_NULLPTR));
        CamshotBtn->setText(QApplication::translate("QtGuiVisionPrjClass", "\346\210\252\345\233\276", Q_NULLPTR));
        OpenCamBtn->setText(QApplication::translate("QtGuiVisionPrjClass", "\346\211\223\345\274\200\346\221\204\345\203\217\345\244\264", Q_NULLPTR));
        CloseCamBtn->setText(QApplication::translate("QtGuiVisionPrjClass", "\345\205\263\351\227\255\346\221\204\345\203\217\345\244\264", Q_NULLPTR));
        campicblack->setText(QApplication::translate("QtGuiVisionPrjClass", "TextLabel", Q_NULLPTR));
        campicwhite->setText(QApplication::translate("QtGuiVisionPrjClass", "TextLabel", Q_NULLPTR));
        campicwhiteshot->setText(QApplication::translate("QtGuiVisionPrjClass", "TextLabel", Q_NULLPTR));
        campicblackshot->setText(QApplication::translate("QtGuiVisionPrjClass", "TextLabel", Q_NULLPTR));
    } // retranslateUi

};

namespace Ui {
    class QtGuiVisionPrjClass: public Ui_QtGuiVisionPrjClass {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_QTGUIVISIONPRJ_H
